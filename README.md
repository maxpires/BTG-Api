# Desafio BTG Engenheiro de Software

  

## Escopo

  

Este projeto tem como objetivo processar pedidos e gerar relatórios. Ele utiliza uma API REST para consulta de dados e um microserviço que consome uma fila no RabbitMQ para salvar os dados processados no banco de dados.

  

## Instruções para Executar a Aplicação

  

### 1. Clone o Repositório

  

Primeiro, clone o repositório para o seu ambiente local:

  

```bash

git clone https://github.com/maxpires/BTG.git

```

### 2. Suba os Contêineres com o Docker Compose

Navegue até o diretório do projeto clonado e execute o seguinte comando para subir todos os contêineres (API, Microserviço, RabbitMQ, Banco de Dados):

```bash

docker-compose up

```

Caso não queira ver os logs e não queria deixar seu terminal travado com a execução use:

```bash

docker-compose up -d

```  
Isso iniciará todos os serviços necessários. Agora, o ambiente está pronto para uso.
### 1. Acesse a API

Após os contêineres estarem em funcionamento, você pode acessar a API no seguinte endereço:

  

http://localhost:5000/index.html

  

A página será aberta com o Swagger, onde você encontrará todos os endpoints disponíveis da API.

  

### 4. Testando o Microserviço com RabbitMQ

Para testar o microserviço que consome a fila RabbitMQ, siga os passos abaixo:
Acesse o RabbitMQ utilizando o seguinte endereço no seu navegador:


http://localhost:15672

Se solicitado acesso, utilize o login e senha padrão:
**Login**: guest
**Senha**: guest

Após fazer login, clique na opção "Queues" no menu.
Localize a fila chamada 'fila_desafio' e clique sobre ela.
Abrirá mais opções. Localize o campo 'payload'.

Cole o seguinte JSON no campo 'payload':

```go
{ 
	"codigoPedido": 1001,
	"codigoCliente":1, 
	"itens": [
		{ 
			"produto": "lápis",
			"quantidade": 100,
			"preco": 1.10 
		},
		{ 
			"produto": "caderno",
			"quantidade": 10,
			"preco": 1.00
		}
	]
}
```
E clique em 'Publish message'
Com isso, você enviou uma mensagem para a fila. O microserviço irá processar essa mensagem e salvar os dados no banco de dados.

### Verificando os Dados
Para verificar se os dados foram salvos no banco de dados, acesse a API novamente  e utilize um dos endpoints disponíveis no Swagger. Por exemplo:

-   **Endpoint**: `/api/v1/Pedidos/`

Digite `1` onde é o **códigoCliente** do JSON que você enviou para a fila. Isso retornará as informações do pedido processado e salvo no banco.

## Tecnologias Utilizadas

-   **.NET 9 (C#)** para a criação da API e microserviço
-   **RabbitMQ** para o gerenciamento da fila de mensagens
-   **SQL Server** para persistência dos dados
