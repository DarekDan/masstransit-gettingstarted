# MassTransit Getting Started
A sample CQRS project with MassTransit Sagas

## Pre-requisites
* .Net 5
* RabbitMQ

### RabbitMQ

#### Run custom RabbitMQ image with Docker
```text
docker run --name masstransit-rabbitmq -d -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```

#### RabbitMQ connection specification
[Official documentation](https://www.rabbitmq.com/uri-spec.html)

### [Serilog](https://serilog.net/)

* [Serilog file based configuration](https://github.com/serilog/serilog-settings-configuration)
* [Serilog Console sink](https://github.com/serilog/serilog-sinks-console)

### [NodaMoney](https://www.nodamoney.org/)

#### [Enterprise Money Pattern](https://martinfowler.com/eaaCatalog/money.html)

### [Steeltoe.io](https://steeltoe.io/)

