IFS=$'\n'
function urldecode() { : "${*//+/ }"; echo -e "${_//%/\\x}"; }

for file in $(cat ../flat-links-v4.txt); do 
    name_clean="$(echo $file | perl -pe 's/.*\/[0-9]+px//g' | perl -pe 's/-Flag_of_//g' | python3 -c "import sys; from urllib.parse import unquote; print(unquote(sys.stdin.read()), end='');" | perl -pe 's/^\-//g' | perl -pe 's/\.svg\.png/.png/g' | perl -pe 's/\(.*\)\.png/.png/g' | perl -pe 's/_\././g' | perl -pe 's/^the_//g')"

    wget -w 3 --no-clobber $file -O "$name_clean"
done

