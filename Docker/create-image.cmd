docker build --no-cache -f Dockerfiles/Dockerfile.Postgres -t servicebussample/postgres Postgres

docker build --no-cache -f Dockerfiles/Dockerfile.Editor -t servicebussample/editor .

docker build --no-cache -f Dockerfiles/Dockerfile.ServiceBus -t servicebussample/servicebus .

docker build --no-cache -f Dockerfiles/Dockerfile.WCFSender1 -t servicebussample/wcfsender1 ../WCFAdapter/ConsoleApp1/WCFSender1.ConsoleHost