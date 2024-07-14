# Product API Project

Este projeto é uma API para gerenciar produtos (CRUD - Create, Read, Update, Delete). Este guia irá ajudá-lo a configurar o ambiente de desenvolvimento, compilar o projeto, executar os testes e rodar a aplicação com Docker Compose. O objetivo é demonstrar conhecimentos em criação de APIs e deploy usando Docker.

## Tecnologias Utilizadas

Este projeto utiliza as seguintes tecnologias:

- **.NET 8**: Framework para desenvolvimento de aplicações web e APIs.
- **Entity Framework Core**: ORM (Object-Relational Mapper) para .NET.
- **Postgres**: Banco de dados relacional.
- **Docker**: Plataforma para desenvolvimento, envio e execução de aplicações em contêineres.
- **Docker Compose**: Ferramenta para definir e gerenciar multi-contêineres Docker.
- **NUnit**: Framework para testes unitários em .NET.
- **Swagger**: Ferramenta para documentação e teste de APIs.
- **Visual Studio**: IDE para desenvolvimento em .NET (opcional).

## Pré-requisitos

Antes de começar, certifique-se de ter o seguinte software instalado em sua máquina:

1. **.NET SDK**: [Download .NET SDK](https://dotnet.microsoft.com/download)
2. **Docker**: [Download Docker](https://www.docker.com/products/docker-desktop)
3. **Docker Compose**: Normalmente já vem incluso com o Docker Desktop.

## Passos para Configuração

### 1. Instalar o .NET SDK

Baixe e instale a versão mais recente do .NET SDK a partir do site oficial: [Download .NET SDK](https://dotnet.microsoft.com/download)

Verifique a instalação abrindo um terminal e executando o comando:

```sh 
dotnet --version 
```

## 2. Instalar o Docker

Baixe e instale o Docker Desktop a partir do site oficial: Download Docker

Verifique a instalação abrindo um terminal e executando o comando:

```sh 
docker --version
```

## 3. Clonar o Repositório

Clone o repositório do projeto para sua máquina local:

```sh 
git clone https://github.com/evtnlife/ProductAPI.git
cd ProductAPI
```

### 4. Compilar o Projeto

Para compilar o projeto, você tem três opções:

1. **Usar o comando `dotnet build`**:
   Na raiz do repositório, abra um terminal e execute o seguinte comando:

   ```bash
   dotnet build
2. **Executar o arquivo build-docker.cmd:**:
    Na raiz do projeto, você pode encontrar o arquivo build-docker.cmd. Execute esse arquivo para compilar o projeto dentro de um contêiner Docker.

    ```bash
    build-docker.cmd
3. **Usar o Visual Studio:**:
    Abra o projeto no Visual Studio. Na barra de menus, selecione Build > Build Solution para compilar o projeto diretamente no ambiente de desenvolvimento integrado.

Isso oferece uma visão clara das diferentes maneiras de compilar o projeto, permitindo ao usuário escolher a opção que melhor se adapta às suas necessidades.
