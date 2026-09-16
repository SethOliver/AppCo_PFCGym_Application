#!/usr/bin/env bash
# =============================================================================
# PFC — feature branch helper
#
#   ./feature.sh start timetable-booking     branch off develop
#   ./feature.sh save  "add capacity check"  stage + commit (prompts for type)
#   ./feature.sh push                        push and print the PR link
#   ./feature.sh finish                      merge into develop and clean up
#
# Branch naming: feature/<name>, so the graph reads clearly when it is marked.
# =============================================================================
set -euo pipefail

CMD="${1:-help}"
ARG="${2:-}"

current() { git rev-parse --abbrev-ref HEAD; }

require_clean() {
  if [[ -n "$(git status --porcelain)" ]]; then
    echo "You have uncommitted changes. Run: ./feature.sh save \"message\""
    exit 1
  fi
}

case "$CMD" in

  start)
    [[ -z "$ARG" ]] && { echo "Usage: ./feature.sh start <short-name>"; exit 1; }
    require_clean
    git checkout develop
    git pull --ff-only origin develop
    git checkout -b "feature/$ARG"
    echo "On feature/$ARG — branched from develop"
    ;;

  save)
    [[ -z "$ARG" ]] && { echo "Usage: ./feature.sh save \"what you did\""; exit 1; }

    echo "Commit type?"
    select TYPE in feat fix style refactor docs test chore; do
      [[ -n "${TYPE:-}" ]] && break
    done

    git add -A
    git status --short
    echo
    read -r -p "Commit the above as '$TYPE: $ARG'? [y/N] " OK
    [[ "$OK" =~ ^[Yy]$ ]] || { echo "Cancelled."; exit 0; }

    git commit -q -m "$TYPE: $ARG"
    echo "Committed on $(current)"
    ;;

  push)
    BRANCH="$(current)"
    git push -u origin "$BRANCH"
    REMOTE_URL="$(git remote get-url origin | sed -E 's#(git@github.com:|https://github.com/)##; s#\.git$##')"
    echo
    echo "Open a pull request into develop:"
    echo "  https://github.com/$REMOTE_URL/compare/develop...$BRANCH"
    ;;

  finish)
    BRANCH="$(current)"
    [[ "$BRANCH" == feature/* ]] || { echo "Not on a feature branch."; exit 1; }
    require_clean

    git checkout develop
    git pull --ff-only origin develop
    # --no-ff keeps the feature branch visible in the history graph, which is
    # the evidence a marker looks for.
    git merge --no-ff "$BRANCH" -m "merge: $BRANCH into develop"
    git push origin develop
    git branch -d "$BRANCH"
    git push origin --delete "$BRANCH" 2>/dev/null || true
    echo "Merged and cleaned up. On develop."
    ;;

  release)
    require_clean
    git checkout main
    git pull --ff-only origin main
    git merge --no-ff develop -m "release: merge develop into main"
    git push origin main
    git checkout develop
    echo "Released to main."
    ;;

  *)
    sed -n '2,14p' "$0" | sed 's/^# \{0,1\}//'
    ;;
esac
