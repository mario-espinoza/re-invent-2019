# SVS407

Goodput vs Throghput -> Servers are too optimistic

## Load Shedding
Avoiding by rejecting requests
Do not waste work
Set timeout for client and function to consistent vatios
Bounded Work:

- input size validation
- paginantion
- checkpoint

tont take exstra work

## Dependency Isolation

Application thread pools
File descriptors
ephemeral ports

Littles Law

Auto scale
warm.start> cold>star

Why:

-

Isolate APIs
Pratec agains tmodeal behaviour

## Queue backlogs

AutoScaling and Lambda
Queue emulates lifo
move old messages to low priority
message ttls for stale information
apply backpressure
sugrge excess traffic
shuffle shardinf

## Operating

xray
cloudwatch insights
Cloudwatch Contributor insights
Cloudwatch Service Lens

Don't take to much wotk
lambda to reject excess work
com
