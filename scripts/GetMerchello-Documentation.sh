##########################################
#
# Automate pull of MarkDown Files
# as part of Merchello Documentation
#
# assumes git init
# assumes git remote add origin https://user:password@github.com/rustyswayne/ExamineMd.git
###########################################
#git init
#git remote add origin https://user:password@github.com/rustyswayne/ExamineMd.git
git pull origin master
git merge origin/master
git checkout -f
