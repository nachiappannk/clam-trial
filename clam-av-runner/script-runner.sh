#!/bin/bash



docker cp $1 my-clam-run:/files-to-be-scaned/
docker exec -w /files-to-be-scaned -it my-clam-run $1
