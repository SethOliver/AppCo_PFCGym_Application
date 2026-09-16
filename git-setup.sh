#!/usr/bin/env bash
# =============================================================================
# PFC — one-time repository setup
#
# Run this ONCE, from the folder containing PFC.sln.
# On Windows use Git Bash (right-click the folder -> "Git Bash Here").
#
#   ./git-setup.sh https://github.com/<user>/<repo>.git "Your Name" you@email.com
#
# Creates main + develop, makes the initial commit, and pushes both.
# =============================================================================
set -euo pipefail

REMOTE="${1:-}"
AUTHOR_NAME="${2:-}"
AUTHOR_EMAIL="${3:-}"

if [[ -z "$REMOTE" ]]; then
  echo "Usage: ./git-setup.sh <remote-url> [name] [email]"
  exit 1
fi

if [[ ! -f PFC.sln ]]; then
  echo "ERROR: run this from the folder containing PFC.sln"
  exit 1
fi

# --- identity -----------------------------------------------------------
# Set per-repo so each teammate's commits are attributed correctly.
if [[ -n "$AUTHOR_NAME" ]]; then
  git init -q 2>/dev/null || true
  git config user.name  "$AUTHOR_NAME"
  git config user.email "$AUTHOR_EMAIL"
fi

# --- init ---------------------------------------------------------------
if [[ ! -d .git ]]; then
  git init -q
  echo "Initialised empty repository"
fi

git symbolic-ref HEAD refs/heads/main

# --- sanity check: never commit build output ---------------------------
if [[ ! -f .gitignore ]]; then
  echo "ERROR: .gitignore is missing. Stopping — you would commit bin/ and obj/."
  exit 1
fi

if git status --porcelain --ignored | grep -qE '^!! .*(bin|obj)/'; then
  echo "Good: bin/ and obj/ are being ignored"
fi

# --- initial commit on main --------------------------------------------
git add .
git commit -q -m "chore: initial project scaffold

ASP.NET Core 8 MVC front end for the PFC gym site.
Ten views, shared layout, in-memory data service, responsive stylesheet."

echo "Committed to main"

# --- develop branch -----------------------------------------------------
# main holds releases only. Day-to-day work merges into develop.
git branch develop
echo "Created develop"

# --- remote -------------------------------------------------------------
if git remote | grep -q '^origin$'; then
  git remote set-url origin "$REMOTE"
else
  git remote add origin "$REMOTE"
fi

git push -u origin main
git push -u origin develop
git checkout develop

echo
echo "Done. You are on develop."
echo "Start work with:  ./feature.sh start <short-name>"
