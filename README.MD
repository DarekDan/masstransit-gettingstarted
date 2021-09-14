# MassTransit Getting Started

## Run custom RabbitMQ image
```text
docker run --name masstransit-rabbitmq -d -p 15672:15672 -p 5672:5672 masstransit/rabbitmq
```

## RabbitMQ connection specification
[Official documentation](https://www.rabbitmq.com/uri-spec.html)

