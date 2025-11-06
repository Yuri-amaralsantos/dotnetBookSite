# 🗨️ **BookRatingSite**

Um site de avaliação de livros com frontend em Vue.js, backend em .NET Core, autenticação JWT, e banco de dados PostgreSQL.
Os livros são obtidos diretamente da API do Google Books, permitindo busca e exibição detalhada de informações.

🚀 Funcionalidades

✅ Busca de Livros integrada à Google Books API
, permitindo encontrar títulos, autores e descrições.

✅ Avaliações de Livros, com notas e comentários personalizados.

✅ Sistema de Reviews de Usuários, onde cada usuário pode comentar e avaliar diferentes livros.

✅ Autenticação de Usuários (Login/Registro com JWT + PostgreSQL).

✅ Sistema Administrativo, com funções para:

Remover comentários inapropriados

Editar informações de livros

✅ API Segura (ASP.NET Core + JWT).

✅ Interface Simples, Responsiva e Intuitiva.
---

## 🛠️ **Como Instalar**

### 1️⃣ **Clonar o Repositório**

git clone https://github.com/Yuri-amaralsantos/bookRatingNetCore.git

---

### 2️⃣ **Configurar o Backend**

cd bookApi
dotnet restore

#### **Alterar o connectionStrings no appsettings.json:**

"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Database=bookDb;Username={usuário};Password={senha}"
},

#### **Iniciar o backend:**

dotnet run

---

### 3️⃣ **Configurar o Frontend**

cd front
npm run dev

---

## 🏗️ Tecnologias Utilizadas

### Frontend:

Vue.js

HTML, CSS, JavaScript

Axios

### Backend:

.NET Core

Swagger

JWT Authentication

### Banco de Dados:

PostgreSQL

### APIs Externas:

Google Books API

---

## 🤝 **Contribuição**

Contribuições são bem-vindas! Sinta-se à vontade para enviar issues e pull requests.

---

## 📜 **Licença**

Este projeto está licenciado sob a Licença MIT
