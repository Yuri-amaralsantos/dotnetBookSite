<template>
  <div class="container">
    <h2>Suas reviews</h2>

    <ul v-if="readBooks.length">
      <li v-for="r in readBooks" :key="r.id" class="review-item">
        <router-link :to="`/books/${r.googleBookId}`">
          <div class="review-card">
            <div class="book-info">
              <img v-if="r.book?.image" :src="r.book.image" alt="Capa do livro" class="thumb" />
              <div>
                <h3>{{ r.book?.title || 'Livro não encontrado' }}</h3>
                <p class="authors">{{ r.book?.authors?.join(', ') }}</p>
                <div class="stars">
                  <StarFilledIcon v-for="n in r.rating" :key="n" class="icon filled small" />
                  <StarIcon v-for="n in 5 - r.rating" :key="'empty-' + n" class="icon small" />
                </div>
              </div>

            </div>


            <div class="comment">
              <p>{{ r.comment }}</p>
            </div>

          </div>
        </router-link>
      </li>
    </ul>

    <p v-else>Nenhuma review ainda.</p>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import Api from '../services/api'
import useAuth from '../services/useAuth'
import { StarFilledIcon, StarIcon } from '@radix-icons/vue'

const readBooks = ref([])
const { token } = useAuth()
const headers = { Authorization: `Bearer ${token.value}` }

onMounted(async () => {
  try {
    const res = await Api.get('/ReadBooks/user', { headers })
    readBooks.value = res.data
  } catch (error) {
    console.error('Erro ao buscar suas reviews', error)
  }
})
</script>

<style scoped>
.container {
  padding: 0 40px;
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.review-card {
  display: flex;
  flex-direction: row;
  gap: 50px;
}

.review-item {
  margin-bottom: 25px;
  padding-bottom: 15px;
  color: var(--color-light-text);
}

.review-item h3 {
  color: var(--color-light-text);
}

.review-item p {
  color: var(--color-bg);
}

.review-item a {
  text-decoration: none;
  color: var(--color-light-text);
  display: block;
}

.book-info {
  display: flex;
  align-items: center;
  gap: 12px;
}

.thumb {
  width: 150px;
  height: 200px;
  object-fit: cover;
  border-radius: 6px;
}

.authors {
  font-size: 13px;
  color: white;
}

.stars {
  display: flex;
  gap: 3px;
  margin: 8px 0;
}

.icon.small {
  width: 20px;
  height: 20px;
  color: #ccc;
}

.icon.filled {
  color: gold;
}

.comment {
  width: 100%;
  border-radius: 8px;
  padding: 1rem;
  background-color: #d8e7e7;
}

.comment p {
  font-weight: 600;
  color: var(--color-secondary);
  font-size: 14px;
}
</style>
