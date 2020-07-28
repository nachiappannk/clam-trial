#/bin/bash

docker create --name my-clam -p 5050:5050 my-clam
docker cp D://text.mp4 my-clam:/text.mp4
docker run my-clam
