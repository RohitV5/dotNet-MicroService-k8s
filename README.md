Seek 2:38 -> 4:18 -> 4:52 (load balancer routing) -> 5:32 (Creating MS SQL Container)

Docker commands

docker build -t rohitv5/platformservice .   


docker run -p 8080:80 -d rohitv5/platformservice 

Command for List of available containers 
docker ps

docker start container_id

To push docker conatiner to docker hub
docker push rohitv5/platformservice 


//Before rolling out new version you need to build the container again and publish it
kubectl rollout restart deployment commands-depl // this will restart the container inside kubernetes





API Gateway : Nodeport will be used for development only otherwise we will use ingress nginx in production

Docs: https://kubernetes.github.io/ingress-nginx/deploy/

kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.7.1/deploy/static/provider/cloud/deploy.yaml



kubectl get namespaces

to check pods under specific namespaces
kubectl get pods --namespace=ingress-nginx


To check the load balancer and its ip address and ports
kubectl get services --namespace=ingress-nginx 


Secret variable for sql username password inside kubernetes
kubectl create secret generic mssql --from-literal=SA_PASSWORD="P@ssw0rd"


In case of ImagePullBackOff error during running kubernetes mssql just pull the image manuallly
docker pull mcr.microsoft.com/mssql/server:2017-latest
and then deleting and re-applying the deployment.

Check all services are running fine by
kubectl get pods

Install MS SQL Server Management Studio Approx 700MB
username is localhost,1433
password: P@ssw0rd

make sure to put username as localhost,1433   I know its wierd

