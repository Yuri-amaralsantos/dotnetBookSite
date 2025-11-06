<template>
    <nav>
        <h1>BookReviews</h1>

        <div class="nav-container">
            <router-link v-if="!isAuthenticated" to="/login">Login</router-link>
            <router-link v-if="!isAuthenticated" to="/register">Registrar</router-link>
            <router-link v-if="isAuthenticated" to="/books">Livros</router-link>
            <router-link v-if="isAuthenticated" to="/mybooks">Perfil</router-link>
        </div>

        <div class="nav-container">
            <span v-if="isAuthenticated">Olá, {{ userName }}!</span>
            <button class="logout" v-if="isAuthenticated" @click="logout">Sair</button>
        </div>
    </nav>
</template>

<script setup>
import { computed } from 'vue'
import { useRouter } from 'vue-router'
import useAuth from '../services/useAuth'

const router = useRouter()
const { auth } = useAuth()

const isAuthenticated = computed(() => auth.isAuthenticated.value)
const userName = computed(() => auth.userName.value)

const logout = () => {
    auth.clearToken()
    router.push('/login')
}
</script>

<style scoped>
nav {
    background-color: #4a6a67;
    padding: 1rem;
    display: flex;
    justify-content: space-between;
    align-items: center;
    gap: 15px;
    color: #e9e8e4;
}

h1 {
    margin: 20px;
    color: #e9e8e4;
}

nav a {
    color: var(--color-light-text);
    text-decoration: none;
    font-size: 20px;
    font-weight: 500;
    margin: 0 10px;
}

nav a.router-link-exact-active {
    border-bottom: 2px solid #585c60;
}

.logout {
    margin: 0 1rem;
    background-color: #e9e8e4;
    color: #000;
    border: none;
    padding: 10px 20px;
    font-weight: bold;
    cursor: pointer;
    transition: 0.2s ease;
}

.logout:hover {
    background-color: #e9e8e4;
}

.nav-container {
    margin: 0;
}
</style>
