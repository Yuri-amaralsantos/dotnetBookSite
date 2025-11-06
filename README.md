# BookRatingSite

## Descrição

Um site de avaliação de livros com frontend em Vue.js, backend em .NET Core, autenticação JWT, e banco de dados PostgreSQL.
Os livros são obtidos diretamente da API do Google Books, permitindo busca e exibição detalhada de informações.

## Tecnologias Utilizadas

### Frontend:

* Vue.js

* HTML, CSS, JavaScript

* Axios

### Backend:

* .NET Core

* Swagger

* JWT Authentication

### Banco de Dados:

* PostgreSQL

### APIs Externas:

* Google Books API

## Funcionalidades

* Busca de Livros integrada à Google Books API
, permitindo encontrar títulos, autores e descrições.

* Avaliações de Livros, com notas e comentários personalizados.

* Sistema de Reviews de livros, onde cada usuário pode comentar e avaliar diferentes livros.

* Autenticação de Usuários (Login/Registro com JWT + PostgreSQL).

* API Segura (ASP.NET Core + JWT).

* Interface simples e intuitiva.

---

## 🛠️ **Como Instalar**

### **Clonar o Repositório**
```bash
git clone https://github.com/Yuri-amaralsantos/bookRatingNetCore.git
``` 
---

### **Configurar o Backend**
```bash
cd backend
dotnet restore
``` 
---

### **Usar o arquivo **`docker-compose.yml`** já configurado no projeoto**:

```bash
docker-compose up -d
``` 

### OU

#### **Altere o connectionStrings no appsettings.json para configurar o seu banco local:**

"ConnectionStrings": {
"DefaultConnection": "Host=localhost;Port=5432;Database=bookDb;Username={usuário};Password={senha}"
},

#### **Iniciar o backend:**
```bash
dotnet run
```
---

### **Configurar o Frontend**
```bash
cd front
npm run install
npm run dev
``` 

---


## 🤝 **Contribuição**

Contribuições são bem-vindas! Sinta-se à vontade para enviar issues e pull requests.


---

## 👨‍💻 Autor

Desenvolvido por **Yuri Amaral Santos**
📧 E-mail: [yuri.a.santos12@gmail.com](mailto:yuri.a.santos12@gmail.com)
🌐 Portfolio: [https://yuri-amaral-santos-portfolio.vercel.app](https://yuri-amaral-santos-portfolio.vercel.app)
