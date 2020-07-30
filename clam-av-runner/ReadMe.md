# About
The code packages a asp.net core web application and clam av antivirus in one docker
The asp.net core web application has an end point to scan a file for antivirus with the clamAV antivirus
The file to be scanned has to be copied manually

## Prerequisite
 * docker is installed
 * cygwin is installed
 * dos2unix package is installed in cygwin

## Steps to build
 * In cygwin, run `dos2unix *.sh`
 * In cygwin, run `dos2unix.exe ./my-web-app/*.sh`
 * In cygwin, run `./build.sh`

## Steps to run
 * In cygwin, run `./run.sh`

### Note
 * This should run the application at localhost:5050
 * This should expose the end point at http://localhost:5050/swagger/index.html
 * After seeing the swagger page wait for about 5 mins to wait for clam av to update the virus signatures
 
## Instructurions for use
 * run a command such as `docker cp <D://text.mp4> my-clam-run:/files-to-be-scaned/text.mp4
 * Through the swagger page, hit the end point `/Clam/files/{name}/scan` with `/files-to-be-scaned/text.mp4` as the parameter
 * The end point should return the scanned details
 
## Instructurions to run a test
 * Run the command `./script-runner.sh ./download-prepare.sh`
 * Run the command `./script-runner.sh ./download-scripts1.sh`
 * Run the command `./test1`
 
# Steps to Stop and Remove
 * In cygwin, run `docker stop my-clam-run  && docker rm my-clam-run`


