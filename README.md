Tech & Beer - Usando Github Actions para fazer deploy no AKS
============================================================

Este repositório foi criado em uma live no youtube, no dia 24/07/2020, no Avanade Tech & Beer Live. O objetivo foi mostrar como utilizar a funcionalidade do Github Actions para fazer deploy no AKS.

A gravação da Live está disponível aqui (já no ínicio da codificação): https://youtu.be/ds2ieSI10Ak?t=2609

Pré-Requisitos
--------------

Para rodar este exemplo, é necessário:

1. Ter o [Azure Cli](https://docs.microsoft.com/pt-br/cli/azure/install-azure-cli?view=azure-cli-latest) instalado
2. Estar com o Azure cli logado na conta que deve ser utilizado e utilizando a subscription correta
3. Ter o [kubectl](https://kubernetes.io/docs/tasks/tools/install-kubectl/) instalado
4. Recomendo a utilização do Visual Studio Code para codificação!

Rodar no seu ambiente
---------------------

Para rodar em seu ambiente, é necessário montar o ambiente, subir o código para o um repositório seu (faça um fork!) e definir os secrets com os valores do seu ambiente.

Comandos para montagem de ambiente
----------------------------------

Para montar o ambiente, os seguinte comandos devem ser rodados no powershell, com o Azure Cli instalado:

1. Obter o id da subscrion atual: ```$subscription = (az account show | ConvertFrom-Json).id```
2. Escolha um nome único para o seu ACR: ```$acrName="meuAcrUnico"```
3. Criar resource group: ```az group create --location eastus --name avanade-tech-beer```
4. Criar ACR: ```az acr create -g avanade-tech-beer --name $acrName --sku basic```
5. Criar AKS: ```az aks create -g avanade-tech-beer --name techbeeraks --node-count 1 --attach-acr $acrName```
6. Criar service principal: ```$servicePrincipal = az ad sp create-for-rbac --sdk-auth --skip-assignment```
7. Converter o Service principal em objeto para uso nos proximos comandos: ```$sp = $servicePrincipal |ConvertFrom-Json```
8. Atribuir o papel de Contributor para o Service Principal poder fazer deploy no AKS: ```az role assignment create --assignee $sp.clientId --scope /subscriptions/$subscription/resourcegroups/avanade-tech-beer/providers/Microsoft.ContainerService/managedClusters/techbeeraks --role Contributor```
9. ```az role assignment create --assignee $sp.clientId  --scope /subscriptions/$subscription/resourceGroups/avanade-tech-beer/providers/Microsoft.ContainerRegistry/registries/$acrName --role AcrPush```
10. Conectar com o AKS localmente: ```az aks get-credentials -g avanade-tech-beer --name techbeeraks```

Cadastrar os secrets no Github
------------------------------

Entre nas configurações do seu repositório do Github, em Secrets e cadastre os seguintes secrets com valores extraídos dos comandos abaixo:

1. AZURE_CREDENTIALS: ```$servicePrincipal```
2. CONTAINER_REGISTRY:  ```$acrName+".azurecr.io"```
3. REGISTRY_USERNAME: ```$sp.clientId```
4. REGISTRY_PASSWORD: ```$sp.clientSecret```

Happy Coding!
