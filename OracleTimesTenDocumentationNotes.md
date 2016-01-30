
# Proposed hands ons #
## Have a windbg lab to see where timesten is located when it runs in the direct link mode. ##

"An application can create a direct driver connection when it runs on the same
machine as the TimesTen database. In a direct driver connection, the ODBC
driver directly loads the TimesTen database into either the application’s heap
space or a shared memory segment. The application then uses the direct driver to
access the memory image of the database. Because no inter-process
communication (IPC) of any kind is required, a direct-driver connection provides
extremely fast performance and is the preferred way for applications to access
TimesTen databases."

# Connection types #
If I understand it correctly then, MS ODBC .NET driver should be using the "Driver manager connection" option. It is only layer of indirection and should still correspond to the direct link if accessed from the same machine. This to be verified by [hands ons](#Proposed.md)

# To clarify #
## What TimesTen windows service is exactly serving for ##
My current bets are: Replication agents, TimesTen Data Manager os ,y next candidate to run out-of in-memory process. One of the things TimesTen Data Manager does is to execute synchronous actions like cache group creation. ? Are cache agents (that are executing the async cache synchronization) running in the off-in-memory?

## Usage of T-tree indexes instead of B-Tree ##
"A typical disk-based
RDBMS uses B-tree indexes to reduce the amount of disk I/O needed to
accomplish indexed look-up of data files. TimesTen uses T-tree indexes, which
are optimized for main memory access. T-tree indexes are more economical than
B-trees because they require less memory space and fewer CPU cycles."
## Cache connect from TT to Oracle. PROPAGATE, investigate on how below two options are different ##
_"Propagate – This is performed by one of the following methods:
– Specifying the PROPAGATE option in the CREATE USERMANAGED
CACHE GROUP statement.
– Creating a synchronous writethrough (SWT) cache group with the
CREATE SYNCHRONOUS WRITETHROUGH CACHE GROUP
statement."_
# Oops! #
## Read committed isolation ##
Desription of Read commited default contradicts to my standard understanding of the READ COMMITTED isolation level:
Oracle:

_"Read committed isolation_


_When an application uses read committed isolation, readers use a separate copy
of the data from writers, so read locks are not needed. Read committed isolation
is non-blocking for queries and can work with Serializable isolation or read
committed isolation. Under read committed isolation, writers block only other
writers and readers using serializable isolation; writers do not block readers using
read committed isolation. Read committed isolation is the default isolation level."_

This corresponds to what I know as SNAPSHOT\_READ\_COMMITTED, READ\_COMMITTED would block until row in question would become comitted. SNAPSHOT would read the last committed value without locks or waiting.

## Cache instances limitations (reasonable) ##
_"A cache instance is the set of rows that are associated by foreign key
relationships with a particular row in the cache group root table. Each primary
key value in the root table specifies a cache instance. Cache instances form the
unit of cache loading and cache aging. No table in the cache group can be a child
to more than one parent in the cache group. Each TimesTen record belongs to
only one cache instance and has only one parent in its cache group."_
**I can understand the simplifying justification behind this, but it may cause some ugly constraints in some cases.**
# Failure and recovery #
## Chechpoints and logs ##
Two types of checkpoints - blocking (fuzzy) and non-blocking.
## Hot Standby ##
## Active Standby ##
## Cases of the Distributed or Split workload ##
## Specifics of return receipt service and return twosafe service ##
## Possible use of the SNMP traps for the DB/App LB switch? ##
_"SNMP traps
Simple Network Management Protocol (SNMP) is a protocol for network
management services. Network management software typically uses SNMP to
query or control the state of network devices like routers and switches. These
devices sometimes also generate asynchronous alerts in the form of UDP/IP
packets, called SNMP traps, to inform the management systems of problems._

_TimesTen cannot be queried or controlled through SNMP. However, TimesTen
sends SNMP traps for certain critical events to facilitate user recovery
mechanisms. TimesTen sends traps for the following events:
• Cache Connect to Oracle autorefresh failure
• Database out of space
• Replicated transaction failure
• Death of daemons
• Database invalidation
• Assertion failure
These events also cause log entries to be written by the TimesTen daemon, but
exposing them through SNMP traps allows properly configured network
management software to take immediate action."_

**I see my own preference currently as either split or distributed workload.** That would support scale out and still provides availability. Concurrency wise one should be very careful how to split the load so there is as little contention as possible. In case of distributed or split workflow DB failure can be implied by the application failure for means of Load Balancing. If workload split/distribute options are too hard to achieve, then my next option would be Hot Standby, that is again because of ability to interpret application failure for the LB switch.
A special test/probing interface can help in cases of specific TimesTen failures that are not causing by itself issues to the application. For example in case of asynchronous replication, that can be the fact that replication backlog it too big.

# Latency #
Discover what are standard and worse case latency models.