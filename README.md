# GlobalSolution

## Integrantes

- **Camila Soares Pedra** - RM98246
- **Gustavo Bertti** - RM552243
- **Gustavo Macedo da Silva** - RM552333
- **Rafael Camargo** - RM551127

## Descrição

O projeto **GlobalSolution** é uma aplicação que gerencia informações sobre dispositivos, consumo de energia, relatórios e usuários, oferecendo funcionalidades como criar, atualizar, excluir e consultar os dados relacionados a esses recursos.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework para desenvolvimento da API.
- **Entity Framework Core**: ORM para interagir com o banco de dados.
- **xUnit**: Framework para testes unitários.
- **Moq**: Biblioteca para mocking em testes.
- **Oracle**: Banco de dados utilizado.

## Estrutura do Projeto

A estrutura do projeto é dividida nas seguintes camadas:

- **Controllers**: Contém os controladores da API.
- **Models**: Contém os modelos que representam as entidades no banco de dados.
- **Persistence**: Contém a configuração do `DbContext` e as interações com o banco de dados.
- **Interfaces**: Define as interfaces de repositórios para acesso a dados.
- **Tests**: Contém os testes automatizados da aplicação.

## Funcionalidades

A aplicação permite:

- Gerenciar dispositivos.
- Monitorar o consumo de energia.
- Gerenciar relatórios.
- Gerenciar usuários.

## Como Rodar o Projeto

1. Clone o repositório para sua máquina local.
   ```bash
   git clone https://github.com/seuusuario/GlobalSolution.git
   
2.Restaure os pacotes NuGet.
  dotnet restore
  
3.Rode o projeto

##Como excutar os testes

1.Execute os testes com o comando:
    dotnet test
