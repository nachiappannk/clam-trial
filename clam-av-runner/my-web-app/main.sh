#!/bin/bash
  
set -m
  
./start &

sleep 20
  
./bootstrap
  

#fg %1
