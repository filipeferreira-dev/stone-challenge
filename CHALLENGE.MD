## [Link Original](https://gist.github.com/vcamaral/78328537a8f3834892ad9779bd267158)

# O problema

Precisamos de uma solução para monitorar a variação de temperatura de determinadas cidades.

# O desafio

## API RESTful

Será necessário construir uma API que permita:

### **Cadastrar uma cidade através de um CEP**

```POST /cities```

```json
{
    "postalCode": "20021-130"
}
```

Resposta:

```json
{
    "data": {
        "key": "69153c60-819c-4b92-992a-0549caf08bea",
        "name": "Rio de Janeiro",
        "postalCode": "20021-130",
        "createdOn": "2019-05-03T09:00:00"
    },
    "success": true
}
```

Requisitos:

- Utilizar a API **Via CEP** para obter o nome da cidade através do CEP informado: https://viacep.com.br/ws/20021-130/json/
- Não deve ser permitido cadastrar duas cidades com o mesmo CEP
- O campo `createdOn` deve ser preenchido automaticamente e estar no formato **ISO 8601 (UTC)**

### **Remover uma cidade**

```DELETE /cities/{key}```

Resposta:

```json
{
    "success": true
}
```

Requisitos:

- A remoção deve ser lógica e não física

### **Listar cidades**

```GET /cities```

Resposta:

```json
{
    "data": [
        {
            "key": "69153c60-819c-4b92-992a-0549caf08bea",
            "name": "Rio de Janeiro",
            "postalCode": "20021-130",
            "createdOn": "2019-05-04T09:00:00"
        },
        {
            "key": "033adc9e-cd91-4a62-bc60-0a2f351cae97",
            "name": "São Paulo",
            "postalCode": "04551-010",
            "createdOn": "2019-05-04T10:00:00"
        }
    ],
    "paging": {
        "page": 1,
        "recordsPerPage": 2,
        "totalPages": 2,
        "totalRecords": 4
    }
}
```

Requisitos:

- A listagem de cidades deve ser paginada
- O número máximo de registros por página deve ser 100
- As cidades devem estar ordenadas de forma decrescente pela data de inserção

### **Cadastrar a temperatura de uma cidade**

```POST /cities/{key}/temperatures```

Resposta:

```json
{
    "data": {
        "city": "Rio de Janeiro",
        "temperature": 30,
        "createdOn": "2019-05-04T09:00:00"
    },
    "success": true
}
```

Requisitos:

- Utilizar a API **HG Weather** para buscar a temperatura atual da cidade informada: https://console.hgbrasil.com/documentation/weather
- O campo `createdOn` deve ser preenchido automaticamente e estar no formato **ISO 8601 (UTC)**

### **Listar as últimas temperaturas de uma cidade**

```GET /cities/{key}/temperatures```

Resposta:

```json
{
    "data": {
        "city": "Rio de Janeiro",
        "temperatures": [
            {
                "temperature": 30,
                "createdOn": "2019-05-04T08:00:00"
            },
            {
                "temperature": 25,
                "createdOn": "2019-05-04T09:00:00"
            }
        ]
    }
}
```

Requisitos:

- Somente as temperaturas registradas nas últimas 24 horas devem ser listadas
- As temperaturas devem estar ordenadas de forma crescente pela data de inserção

### **Listar as últimas temperaturas de todas as cidades**

```GET /cities/{key}/temperatures```

Resposta:

```json
{
    "data": [
        {
            "city": "Rio de Janeiro",
            "temperatures": [
                {
                    "temperature": 30,
                    "createdOn": "2019-05-04T08:00:00"
                },
                {
                    "temperature": 25,
                    "createdOn": "2019-05-04T09:00:00"
                }
            ]
        },
        {
            "city": "São Paulo",
            "temperatures": [
                {
                    "temperature": 30,
                    "createdOn": "2019-05-04T08:00:00"
                },
                {
                    "temperature": 25,
                    "createdOn": "2019-05-04T09:00:00"
                }
            ]
        }
    ]
}
```

Requisitos:

- Somente as temperaturas registradas nas últimas 24 horas devem ser listadas
- As temperaturas devem estar ordenadas de forma crescente pelo nome da cidade e pela data de inserção

### **Apagar o histórico de temperaturas de uma cidade**

```DELETE /cities/{key}/temperatures```

Resposta:

```json
{
    "success": true
}
```

- A remoção deve ser lógica e não física

## Monitoramento da variação de temperatura

Será necessário também construir uma aplicação que será executada automaticamente e irá registrar a temperatura atual de cada cidade cadastrada.

Inicialmente, a aplicação deverá registrar as temperaturas a cada hora, mas isso deve ser configurável e permitir que a periodicidade seja alterada de forma simples.

**A aplicação não poderá acessar diretamente o banco de dados. Todo o acesso aos dados deverá ser realizado através da API construída.**

# Requisitos

- O desenvolvimento da API deve ser feito utilizando ASP.NET Web API (C#)
- Não é permitido a utilização de nenhum tipo de ORM para acesso ao banco de dados
- Testes unitários
- Atenção para utilizar os status codes do HTTP corretamente de acordo com cada operação da API
- O código final deve estar versionado e publicado em um repositório no GitHub
    - Este repositório deve conter um arquivo `README.md` com as instruções necessárias para a execução da API e da aplicação responsável por cadastrar as temperaturas

# Critérios de avaliação

- Arquitetura e organização do projeto
- Legibilidade do código
- Utilização de padrões e boas práticas de programação