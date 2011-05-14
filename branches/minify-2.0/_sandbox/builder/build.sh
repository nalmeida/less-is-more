#!/bin/bash

ARGS="";
for par in "$@";
do
	ARGS="$ARGS -D$par";
done;

export ANT_OPTS="$ANT_OPTS -Dfile.encoding=UTF-8";
echo "Running: ant $ARGS"
ant $ARGS