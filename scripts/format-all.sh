#!/bin/bash

exclude_list=(
    "Packages"
)

CLANG_FORMAT_CONFIG="$(pwd)/.clang-format"
export CLANG_FORMAT_CONFIG
FILES=$(git ls-files "*.cs")

for exclude in "${exclude_list[@]}"; do
    FILES=$(echo "$FILES" | grep -v --invert-match "^$exclude/")
done

# Set the Internal Field Separator to newline
IFS=$'\n'
for file in $FILES; do
    echo "Formatting $file"
    echo "Using clang-format config: $CLANG_FORMAT_CONFIG"
    clang-format-11 -style=file -i -assume-filename="$file" "$file"
done

echo "Formatting complete!"