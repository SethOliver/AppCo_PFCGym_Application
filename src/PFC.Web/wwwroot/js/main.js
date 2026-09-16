/* PFC — front-end behaviour
   Vanilla JS, no dependencies. Every block guards for missing elements so
   the same file can be loaded on every page. */
(function () {
  'use strict';

  /* ---------- 1. Mobile navigation overlay ---------- */
  var burger = document.querySelector('.burger');
  var panel = document.querySelector('.navpanel');

  if (burger && panel) {
    var closeBtn = panel.querySelector('.navpanel__close');
    var lastFocused = null;

    function openNav() {
      lastFocused = document.activeElement;
      panel.classList.add('is-open');
      burger.setAttribute('aria-expanded', 'true');
      document.body.style.overflow = 'hidden';
      if (closeBtn) closeBtn.focus();
    }

    function closeNav() {
      panel.classList.remove('is-open');
      burger.setAttribute('aria-expanded', 'false');
      document.body.style.overflow = '';
      if (lastFocused) lastFocused.focus();
    }

    burger.addEventListener('click', openNav);
    if (closeBtn) closeBtn.addEventListener('click', closeNav);

    document.addEventListener('keydown', function (e) {
      if (e.key === 'Escape' && panel.classList.contains('is-open')) closeNav();
    });

    // trap Tab inside the panel while it is open
    panel.addEventListener('keydown', function (e) {
      if (e.key !== 'Tab' || !panel.classList.contains('is-open')) return;
      var items = panel.querySelectorAll('a, button');
      if (!items.length) return;
      var first = items[0];
      var last = items[items.length - 1];
      if (e.shiftKey && document.activeElement === first) {
        e.preventDefault();
        last.focus();
      } else if (!e.shiftKey && document.activeElement === last) {
        e.preventDefault();
        first.focus();
      }
    });
  }

  /* ---------- 2. Timetable day switching ---------- */
  var dayBtns = document.querySelectorAll('.day');

  if (dayBtns.length) {
    function showDay(key) {
      dayBtns.forEach(function (b) {
        b.setAttribute('aria-selected', String(b.dataset.day === key));
      });
      document.querySelectorAll('[data-day-panel]').forEach(function (p) {
        p.hidden = p.dataset.dayPanel !== key;
      });
      var label = document.querySelector('[data-day-label]');
      var active = document.querySelector('.day[aria-selected="true"]');
      if (label && active) {
        var panelEl = document.querySelector('[data-day-panel="' + key + '"]');
        var count = panelEl ? panelEl.querySelectorAll('.slot').length : 0;
        label.textContent = active.dataset.full + ' · ' + count +
          (count === 1 ? ' class' : ' classes');
      }
    }

    dayBtns.forEach(function (btn) {
      btn.addEventListener('click', function (e) {
        // The server already renders the right day, so this is pure enhancement:
        // switch instantly instead of making a round trip. Without JS the link
        // still works and the controller returns the correct day.
        e.preventDefault();
        showDay(btn.dataset.day);
        if (btn.href) history.replaceState(null, '', btn.href);
      });
    });

    var selected = document.querySelector('.day[aria-selected="true"]');
    if (selected) showDay(selected.dataset.day);
  }

  /* ---------- 3. Password visibility ---------- */
  document.querySelectorAll('.toggle-pw').forEach(function (btn) {
    btn.addEventListener('click', function () {
      var input = document.getElementById(btn.dataset.target);
      if (!input) return;
      var showing = input.type === 'text';
      input.type = showing ? 'password' : 'text';
      btn.textContent = showing ? 'Show' : 'Hide';
      btn.setAttribute('aria-label', (showing ? 'Show' : 'Hide') + ' password');
    });
  });

  /* ---------- 4. Form validation ---------- */
  var RULES = {
    name: { test: function (v) { return v.trim().length >= 2; },
            msg: 'Please enter your full name.' },
    email: { test: function (v) { return /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/.test(v.trim()); },
             msg: 'Enter a valid email address, e.g. you@example.co.za' },
    phone: { test: function (v) { return v.trim() === '' || /^[+\d][\d\s()-]{8,}$/.test(v.trim()); },
             msg: 'Enter a valid phone number, or leave it blank.' },
    password: { test: function (v) { return v.length >= 8; },
                msg: 'Password must be at least 8 characters.' },
    message: { test: function (v) { return v.trim().length >= 10; },
               msg: 'Tell us a little more — at least 10 characters.' }
  };

  function validateField(input) {
    var rule = RULES[input.dataset.rule];
    if (!rule) return true;
    var ok = rule.test(input.value);
    var errEl = input.closest('.field').querySelector('.error');
    input.setAttribute('aria-invalid', String(!ok));
    if (errEl) errEl.textContent = ok ? '' : rule.msg;
    return ok;
  }

  document.querySelectorAll('form[data-validate]').forEach(function (form) {
    var fields = form.querySelectorAll('[data-rule]');

    fields.forEach(function (input) {
      input.addEventListener('blur', function () { validateField(input); });
      input.addEventListener('input', function () {
        if (input.getAttribute('aria-invalid') === 'true') validateField(input);
      });
    });

    form.addEventListener('submit', function (e) {
      var valid = true;
      var firstBad = null;

      fields.forEach(function (input) {
        if (!validateField(input)) {
          valid = false;
          if (!firstBad) firstBad = input;
        }
      });

      var err = form.querySelector('.alert--err');

      if (!valid) {
        // Block the round trip only when we already know it would fail.
        // The server re-validates everything regardless, so turning JS off
        // loses the instant feedback but never the validation itself.
        e.preventDefault();
        if (err) err.classList.add('is-shown');
        if (firstBad) firstBad.focus();
        return;
      }

      if (err) err.classList.remove('is-shown');

      var btn = form.querySelector('[type="submit"]');
      if (btn) {
        btn.disabled = true;
        btn.textContent = 'Sending…';
        // Re-enable if the browser restores this page from cache on Back.
        window.addEventListener('pageshow', function () {
          btn.disabled = false;
          btn.textContent = btn.dataset.label || 'Submit';
        });
      }
    });
  });

  /* ---------- 5. Scroll reveal ---------- */
  var reveals = document.querySelectorAll('.reveal');

  if (reveals.length && 'IntersectionObserver' in window) {
    var io = new IntersectionObserver(function (entries) {
      entries.forEach(function (entry) {
        if (entry.isIntersecting) {
          entry.target.classList.add('is-visible');
          io.unobserve(entry.target);
        }
      });
    }, { rootMargin: '0px 0px -60px 0px' });
    reveals.forEach(function (el) { io.observe(el); });
  } else {
    reveals.forEach(function (el) { el.classList.add('is-visible'); });
  }

})();
