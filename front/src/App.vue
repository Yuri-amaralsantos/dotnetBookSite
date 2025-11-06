<template>
  <div id="app">
    <Navbar />
    <router-view />
  </div>
</template>

<script setup>
import Navbar from './components/Navbar.vue';
</script>

<style>
h2,
h3 {
  color: #585c60;
  font-weight: 600;
}

.book-card {
  background-color: #089790;
  padding: 15px;
  border-radius: 10px;
  display: flex;
  flex-direction: column;
  align-items: center;
  max-width: 200px;
  color: var(--color-light-text);
}
</style>
