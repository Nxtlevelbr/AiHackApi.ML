# AiHackApiML.Net

## Equipe de Desenvolvimento

- RM99841 - Marcel Prado Soddano
- RM551261 - Giovanni Sguizzardi
- RM98057 - Nicolas E. Inohue
- RM552302 - Samara Moreira
- RM552293 - Vinicius Monteiro

## Descrição do Projeto

AiHackApi é uma API RESTful construída com ASP.NET Core para gerenciar consultas médicas, recomendação de especialistas e outros serviços de saúde. Além das operações CRUD tradicionais, a aplicação oferece uma funcionalidade de recomendação de especialistas médicos com base nos sintomas informados pelo usuário, utilizando **ML.NET** para personalizar a experiência do usuário e otimizar o encaminhamento médico.

### Funcionalidades Principais
1. **Gestão de Consultas Médicas**: CRUD completo para consultas, pacientes, médicos, endereços e contatos.
2. **Sistema de Recomendação**: Identificação de médicos especialistas recomendados com base nos sintomas descritos pelo usuário,
3.  utilizando modelos de machine learning.
4. **Integração com Banco de Dados Oracle**: Interação direta com o banco de dados para armazenar e recuperar informações de saúde.
5. **Documentação Interativa**: Documentação via Swagger/OpenAPI para facilitar a exploração interativa dos endpoints.

## Arquitetura

O projeto adota uma arquitetura monolítica em camadas para centralizar componentes e facilitar a manutenção, com o uso de boas práticas de Clean Code
e princípios SOLID. Essa estrutura atende ao contexto atual do projeto, garantindo simplicidade, eficiência e modularidade.

### Camadas da Aplicação
- **Controllers**: Responsáveis por receber requisições HTTP, validar dados básicos e delegar a lógica para a camada de serviços.
- **Services**: Contêm a lógica de negócios, incluindo o sistema de recomendação de especialistas. Aqui, são aplicadas as regras de negócio e
-  interações com o modelo de machine learning.
- **Repositories**: Gerenciam o acesso ao banco de dados, encapsulando operações de leitura e escrita.
- **Models**: Representam as entidades (Paciente, Consulta, Médico, etc.) e fazem o mapeamento para as tabelas no banco de dados.
- **DTOs**: Estruturas de dados usadas para comunicação entre camadas, garantindo segurança e eficiência no tráfego de dados.

### Padrões de Projeto Utilizados
- **Dependency Injection**: Facilita a substituição de implementações, tornando o sistema mais flexível e testável.
- **Repository Pattern**: Mantém a lógica de persistência isolada, promovendo o reaproveitamento de código e a independência da camada de dados.
- **Factory Pattern**: Usado para a criação de objetos relacionados ao banco de dados de forma eficiente.
- **Singleton**: Garante que configurações críticas sejam carregadas uma única vez durante a execução.

## Sistema de Recomendação de Especialistas

O sistema de recomendação utiliza **ML.NET** para analisar os sintomas fornecidos pelo usuário e sugerir o médico especialista mais adequado.
Este modelo de machine learning permite que o sistema aprenda e melhore continuamente, garantindo que as recomendações sejam cada vez mais 
precisas e personalizadas.

### Como Utilizar a Recomendação
1. **Endpoint**: POST /api/recomendacao
2. **Parâmetros**: Enviar uma descrição dos sintomas.
3. **Retorno**: Um JSON com o especialista recomendado para atender o caso.

### ML.NET
O modelo foi treinado e integrado utilizando ML.NET, com foco em:
- **Recomendação de Especialistas**: Baseada em sintomas específicos.
- **Análise de Sentimento**: Para compreender melhor as necessidades dos pacientes e aprimorar o serviço.

## Documentação Interativa

O Swagger permite a documentação e teste interativo dos endpoints. Acesse em: `http://localhost:{5066}/swagger`

## Instruções para Executar a API

### Pré-requisitos
- **.NET SDK 8.0**: Verifique com `dotnet --version`.
- **Banco de Dados Oracle**: Deve estar configurado e em execução.

# Passos para Executar a Aplicação

# Clone o Repositório
# Abra o terminal e execute o comando abaixo para clonar o repositório em sua máquina local:
git clone git@github.com:Nxtlevelbr/AiHackApi.ML.git

# Acesse o Diretório do Projeto
# Navegue até o diretório recém-clonado:
cd AiHackApi.ML

# Instale as Dependências
# Exemplo para projetos .NET:
dotnet restore

# Compile o Projeto
# Se o projeto precisar de compilação, execute o comando:
dotnet build

# Execute a Aplicação
# Após compilar, inicie a aplicação com o comando:
dotnet run

# Acesse a Aplicação (atualizar a pagina se preciso)
# Acesse a aplicação no navegador ou em um cliente API pelo endereço padrão:
# Geralmente é http://localhost:5066 ou conforme especificado nas configurações do projeto.

- RM99841 - Marcel Prado Soddano
- RM551261 - Giovanni Sguizzardi
- RM98057 - Nicolas E. Inohue
- RM552302 - Samara Moreira
- RM552293 - Vinicius Monteiro

