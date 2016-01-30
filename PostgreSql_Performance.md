## profiler ##

To get a definitve answer on how much time is spent in database
processing, set log\_min\_duration\_statement to 0 (default is -1 =
disabled) in your postgresql.conf, kill -HUP the database, and re-run
your request. You'll get durations for every single statement. It is
very easy to overlook a performance killer here. If you find a statement
that takes seconds, run an "explain" on it to make sure it uses an
index, or create an appropriate index if it doesn't.