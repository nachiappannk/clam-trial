#!/bin/bash
  
set -m
  
./script-bootstrap-asp.sh &

sleep 20
  
./script-bootstrap.sh

#fg %1
