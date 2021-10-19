# MassTransit Getting Started
A sample CQRS project with MassTransit Sagas

## Pre-requisites
* .Net 5
* RabbitMQ
* Redis

### RabbitMQ

#### Run custom RabbitMQ image with Docker
```text
docker run --name masstransit-rabbitmq -d -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```

#### RabbitMQ connection specification

[Official documentation](https://www.rabbitmq.com/uri-spec.html)

### Redis
#### Run Redis image with Docker
```text
docker run --name masstransit-redis -p 6379:6379  -d redis
```

### [Serilog](https://serilog.net/)

* [Serilog file based configuration](https://github.com/serilog/serilog-settings-configuration)
* [Serilog Console sink](https://github.com/serilog/serilog-sinks-console)

### [NodaMoney](https://www.nodamoney.org/)

#### [Enterprise Money Pattern](https://martinfowler.com/eaaCatalog/money.html)

### [Steeltoe.io](https://steeltoe.io/)

### Apache Benchmark
Once a sample file is available, a load test can be performed with teh assistance from 
[ApacheBenchmark](https://httpd.apache.org/docs/2.4/programs/ab.html) tool:
```txt
ab -c 4 -n 10000 -T "application/json" -p sample.json https://localhost:5001/Payments
```
Sample result:
```txt
This is ApacheBench, Version 2.3 <$Revision: 1879490 $>
Copyright 1996 Adam Twiss, Zeus Technology Ltd, http://www.zeustech.net/
Licensed to The Apache Software Foundation, http://www.apache.org/

Benchmarking localhost (be patient)
Completed 1000 requests
Completed 2000 requests
Completed 3000 requests
Completed 4000 requests
Completed 5000 requests
Completed 6000 requests
Completed 7000 requests
Completed 8000 requests
Completed 9000 requests
Completed 10000 requests
Finished 10000 requests


Server Software:        Kestrel
Server Hostname:        localhost
Server Port:            5001
SSL/TLS Protocol:       TLSv1.2,ECDHE-RSA-AES256-GCM-SHA384,2048,256
Server Temp Key:        ECDH P-256 256 bits
TLS Server Name:        localhost

Document Path:          /Payments
Document Length:        0 bytes

Concurrency Level:      4
Time taken for tests:   118.817 seconds
Complete requests:      10000
Failed requests:        0
Total transferred:      1110000 bytes
Total body sent:        2310000
HTML transferred:       0 bytes
Requests per second:    84.16 [#/sec] (mean)
Time per request:       47.527 [ms] (mean)
Time per request:       11.882 [ms] (mean, across all concurrent requests)
Transfer rate:          9.12 [Kbytes/sec] received
                        18.99 kb/s sent
                        28.11 kb/s total

Connection Times (ms)
              min  mean[+/-sd] median   max
Connect:       17   38  11.7     36     106
Processing:     4    9   3.2     10      82
Waiting:        4    9   2.9     10      59
Total:         22   47  11.8     46     118

Percentage of the requests served within a certain time (ms)
  50%     46
  66%     50
  75%     56
  80%     58
  90%     64
  95%     69
  98%     75
  99%     78
 100%    118 (longest request)
```
