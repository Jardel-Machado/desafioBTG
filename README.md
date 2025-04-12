# Desafio BTG – Processamento de Pedidos com RabbitMQ e API REST

Este projeto tem como objetivo atender aos requisitos de um desafio técnico que simula o fluxo de pedidos realizados por clientes, processando mensagens via RabbitMQ, persistindo os dados em um banco de dados MySQL, e disponibilizando informações por meio de uma API RESTful.

---

## 🧩 Escopo do Desafio

O sistema foi dividido em três domínios principais:

- **Clientes**
- **Pedidos**
- **Itens de Pedido**

Cada pedido é enviado via fila, associado a um cliente e contendo múltiplos itens. O consumidor da fila persiste os dados na base e disponibiliza informações por API.

---

## 📬 Payload da Fila

As mensagens recebidas pela fila `btg-pactual-order-created` têm o seguinte formato:

```json
{
  "codigoPedido": 1001,
  "codigoCliente": 1,
  "itens": [
    { "produto": "lápis", "quantidade": 100, "preco": 1.10 },
    { "produto": "caderno", "quantidade": 10, "preco": 1.00 }
  ]
}
```

---

## ✅ Soluções implementadas

### 🛠️ Processamento e persistência

- Consumidor RabbitMQ (`PedidoConsumer`) escuta a fila, desserializa o JSON e processa com base no domínio.
- Verificação para **evitar duplicidade de clientes e pedidos** com base em `CodigoCliente` e `CodigoPedido`.
- Criação do cliente caso ainda não exista.
- Criação do pedido e associação dos itens.

---

### 🗃️ Modelagem de domínio (NHibernate)

- Separação de responsabilidades em **Clientes**, **Pedidos** e **ItensPedido**.
- Adição de campos `CodigoCliente` e `CodigoPedido` para controle externo.
- Ajuste do banco para permitir `Nome` nulo em clientes (nome não vem da fila).
- Relacionamentos e validações encapsuladas nas entidades.

---

### 🧪 Endpoints RESTful

Os seguintes endpoints foram implementados conforme exigido:

| Recurso                                              | Descrição                                  |
|------------------------------------------------------|--------------------------------------------|
| `GET /api/v1/pedidos/{codigo}/valor-total`           | Retorna o valor total do pedido            |
| `GET /api/v1/clientes/{codigo}/quantidade-pedidos`   | Retorna a quantidade de pedidos por cliente|
| `GET /api/v1/clientes/{codigo}/pedidos`              | Retorna a lista de pedidos realizados      |

---

### 💡 Tecnologias utilizadas

- .NET 8  
- NHibernate 5.5.2  
- RabbitMQ.Client  
- MySQL 8  
- FluentNHibernate  
- System.Text.Json  

---

### ⚠️ Desafios resolvidos

- Correção de erro `Dialect does not support DbType.Decimal` com uso do `MySQL8Dialect`
- Desserialização `camelCase → PascalCase` com `PropertyNameCaseInsensitive = true`
- Isolamento de serviços por domínio com instanciamento protegido
- Materialização inteligente para evitar limitações de conversão de tipos no LINQ
- Consumer resiliente e desacoplado com `IServiceScopeFactory`

---

## 🚀 Como testar

1. Envie uma mensagem para a fila `btg-pactual-order-created`
2. Verifique se os dados foram persistidos corretamente no banco
3. Acesse os endpoints REST para consultar os dados do pedido

---

## 📁 Estrutura de pastas

```
DesafioBtg/
│
├── API/                     # Endpoints REST
├── Aplicacao/              # Camada de aplicação (opcional)
├── Dominio/
│   ├── Clientes/
│   ├── Pedidos/
│   └── ItensPedidos/
├── Infra/
│   └── Mapeamentos/        # FluentNHibernate maps
├── Consumers/
│   └── PedidoConsumer.cs
└── DataTransfer/           # DTOs e comandos
```

---

## 👨‍💻 Autor

**Jardel Machado Pereira**  
GitHub: [@Jardel-Machado](https://github.com/Jardel-Machado)
LinkedIn: [@jardel-machado](https://www.linkedin.com/in/jardel-machado-pereira-62257611a/)