#!/bin/bash
  
set -m
  
./script-bootstrap-asp.sh &

sleep 20
  
/bootstrap.sh

#fg %1
