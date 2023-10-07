# Projetos

Os projetos constru�dos nessa solu��o s�o:

* [Producer](#producer)
* [Consumers](#consumers)
* [Domain](#domain)
* [Infrastructure](#infrastructure)
* [WebApi](#webapi)
* [WebApp](#webapp)
* [DockerCompose](#dockercompose)

## Producer

O Producer constr�i uma mensagem para ser enviado a determinado t�pico do Kafka.

O servi�o utilizado para enviar a mensagem tenta conectar ao Kafka em loop no seu construtor, 
evitando que ele tente enviar uma mensagem caso o kafka esteja offline e
a mensagem se perca.

O producer n�o possui refer�ncia a nenhum outro projeto, sendo utilizado apenas
como servi�o pela WebApi.

## Consumers

Todos os consumers possuem o objetivo de direcionar as mensagens para o
destinat�rio correto, por exemplo, uma mensagem que almeja enviar foto de um
gato por email, n�o deve enviar outro tipo de foto e nem enviar foto de gato
para o mobile.

Por isso, cada consumer � respons�vel pelo seu pr�prio t�pico. Dessa forma,
caso o programa precise escalar (receber fotos de outros animais, por exemplo),
o consumer pode ser criado ap�s os novos t�picos, pois o kafka ir� armazenar
as mensagens mesmo que n�o haja nenhum consumer dispon�vel. Al�m disso,
caso um consumer fique offline, n�o afetar� todos envios de fotos, apenas
daquele consumer espec�fico.

Os consumers possuem uma seguran�a extra para se manterem em loop sempre
recebendo mensagens e garantir que caso uma mensagem n�o seja enviada, ela
poder� ser consumida novamente para tentar uma nova entrega.

## Domain

Nesta solu��o, o Domain possui apenas as entidades e Dtos. Sem refer�ncia
a nenhum outro projeto.

## Infrastructure

O Infrastructure possui como refer�ncia o Domain, e serve como uma interface
para acesso ao banco de dados principal, incluindo servi�os de seguran�a como
manipula��o do JwT para autorizar acesso aos servi�os mais sens�veis,
Identity para cria��o e utiliza��o das contas de usu�rios, mapeamentos entre
Dto e entidades e o pr�prio reposit�rio para manipula��o do banco de dados.


## WebApi

A WebApi possui refer�ncia direta ao Infrastructure e ao Producer, utilizando
os servi�os dispon�veis conforme necess�rio atrav�s de inje��o de depend�ncia.

� respons�vel pela comunica��o entre a aplica��o Web, a Infrastructure e o
Producer, atrav�s de endpoints protegidos para serem acessados apenas
por requisi��o http pela aplica��o Web.

## WebApp

A Aplica��oWeb n�o possui refer�ncia com nenhum outro projeto e apenas faz requisi��es
diretamente para a WebApi, consumindo tokens atrav�s de sess�es para conseguir
autoriza��o para liberar suas funcionalidades, que inclui cria��o de conta, login,
enviar fotos e se cadastrar para receber fotos pelos t�picos dispon�veis.

� a �nica parte de toda a solu��o que fica exposta ao usu�rio e lida com todas
a intera��es deste com os demais servi�os.

## DockerCompose

Todos os projetos s�o orquestrados por um �nico docker-compose e um Dockerfile
para cada (exceto pelo Domain e Infrastructure).

Al�m dos projetos do ecossistema .NET, o docker-compose tamb�m possui
uma imagem para o Kafka e outra para o PostgreSQL.

� utilizado um arquivo .env para os valores sens�veis utilizados nesta solu��o,
como dados para acesso ao banco de dados, API Keys e outros.