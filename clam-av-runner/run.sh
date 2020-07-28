#/bin/bash

docker run -d --name my-clam-run -p 5050:5050 my-clam
docker cp D://text.mp4 my-clam-run:/text.mp4

