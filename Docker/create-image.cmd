docker build --no-cache -f Dockerfiles/Dockerfile.Postgres -t servicebussample/postgres Postgres

docker build --no-cache -f Dockerfiles/Dockerfile.Editor -t servicebussample/editor .

docker build --no-cache -f Dockerfiles/Dockerfile.ServiceBus -t servicebussample/servicebus .

docker build --no-cache -f Dockerfiles/Dockerfile.WCFSender1 -t servicebussample/wcfsender1 ../WCFAdapter/ConsoleApp1/WCFSender1.ConsoleHost

rem docker build --no-cache -f Dockerfiles/Dockerfile.WCFListener1 -t servicebussample/wcflistener1 ../WCFAdapter/ConsoleApp2/WCFListener1.ConsoleHost

docker build --no-cache -f Dockerfiles/Dockerfile.WCFRecipient1 -t servicebussample/wcfrecipient1 ../WCFAdapter/ConsoleApp3/WCFRecipient1.ConsoleHost

docker build --no-cache -f Dockerfiles/Dockerfile.RESTSender1 -t servicebussample/restsender1 ../RESTAdapter/MsgSender/RestSender1.ConsoleHost

docker build --no-cache -f Dockerfiles/Dockerfile.RESTRecipient1 -t servicebussample/restrecipient1 ../RESTAdapter/MsgRecipient/RestRecipient.ConsoleHost