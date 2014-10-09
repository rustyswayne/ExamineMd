##########################################
#
# Automate pull of MarkDown Files
# as part of Merchello Documentation
#
# assumes git init
# assumes git remote add origin https://user:password@github.com/rustyswayne/ExamineMd.git
#
# %1 username
# %2 password
#
# throws error message is 2 params aren't passed
###########################################
#git init
git remote add origin https://$1:$2@github.com/Merchello/Merchello-Documentation.git

if [ $# -eq 2 ]; then

cd ../..
cd Merchello-Documentation
git pull origin master
git merge origin/master
git checkout -f

else

	echo Username and Password were not passed in

fi

exit