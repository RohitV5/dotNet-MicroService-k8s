Seek 2:38 -> 4:18 -> 4:52 (load balancer routing)

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