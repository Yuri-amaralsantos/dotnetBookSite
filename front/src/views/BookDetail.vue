<template>
  <div v-if="book" class="container">
    <div class="book-header">

      <div class="book-info">
        <h2>{{ book.title }}</h2>
        <p>Autor:{{ book.authors?.join(', ') }}</p>
        <p>Publicado: {{ book.publishedDate }}</p>
      </div>
      <img :src="book.image" alt="Capa do livro" />
    </div>

    <form @submit.prevent="submitReview" class="review-form">
      <div class="rating-stars">
        <span v-for="n in 5" :key="n" class="star" @click="setRating(n)">
          <StarFilledIcon v-if="n <= rating" class="icon filled" />
          <StarIcon v-else class="icon" />
        </span>
      </div>

      <div class="comment">
        <input v-model="comment" placeholder="Comentário"></input>
        <button type="submit">Enviar review</button>
      </div>
    </form>

    <div class="other-reviews">
      <h3>Outras reviews</h3>
      <ul v-if="reviews.length">
        <li v-for="r in reviews" :key="r.id" class="review-card">
          <div class="review-info">
            <p><b>{{ r.userName }}</b></p>
            <p>
              <StarFilledIcon v-for="n in r.rating" :key="n" class="icon filled small" />
              <StarIcon v-for="n in 5 - r.rating" :key="'empty-' + n" class="icon small" />
              ({{ r.rating }}/5)
            </p>
          </div>

          <div class="review-comment">
            <p>{{ r.comment }}</p>
          </div>

        </li>
      </ul>
      <p v-else>Nenhuma review ainda.</p>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import Api from '../services/api'
import useAuth from '../services/useAuth'


import { StarFilledIcon, StarIcon } from '@radix-icons/vue'

const route = useRoute()
const book = ref(null)
const reviews = ref([])
const rating = ref(0)
const comment = ref('')
const { token } = useAuth()
const headers = { Authorization: `Bearer ${token.value}` }

async function fetchBook() {
  try {
    const res = await Api.get(`/Books/${route.params.id}`)
    const data = res.data

    book.value = {
      title: data.volumeInfo.title,
      authors: data.volumeInfo.authors,
      publishedDate: data.volumeInfo.publishedDate,
      image: data.volumeInfo.imageLinks?.thumbnail
    }

    const rev = await Api.get(`/ReadBooks/book/${route.params.id}`)
    reviews.value = rev.data
  } catch (error) {
    console.error('Erro ao buscar livro ou reviews', error)
  }
}

function setRating(n) {
  rating.value = n
}

async function submitReview() {
  try {
    await Api.post(
      '/ReadBooks',
      {
        GoogleBookId: route.params.id,
        Rating: rating.value,
        Comment: comment.value
      },
      { headers }
    )

    rating.value = 0
    comment.value = ''
    await fetchBook()
  } catch (error) {
    console.error('Erro ao enviar review', error)
  }
}

onMounted(fetchBook)
</script>

<style scoped>
.container {
  padding: 0 40px;
  display: flex;
  flex-direction: column;
  gap: 60px;
}

.book-info h2 {
  font-size: 40px;
}

.book-info p {
  font-size: 14px;
}

.book-header {
  gap: 30px;
  margin: 20px;
  display: flex;
  justify-content: space-between;
}

.comment {
  display: flex;
  flex-direction: row;
  align-items: center;
  gap: 20px;
}

input {
  margin: 0;
  height: 30px;
}

.rating-stars {
  display: flex;
  align-items: center;
  margin-bottom: 10px;
}

.star {
  cursor: pointer;
  margin-right: 4px;
  transition: transform 0.2s ease;
}

.star:hover {
  transform: scale(1.15);
}

.icon {
  width: 28px;
  height: 28px;
  color: var(--color-secondary);
}

.icon.filled {
  color: gold;
}

.icon.small {
  width: 20px;
  height: 20px;
}

.review-card {
  width: 100%;
  border-radius: 8px;
  padding: 1rem;
  display: flex;
  flex-direction: column;

}

.review-info p {
  color: white;
}

.review-comment {
  background-color: #d8e7e7;
  padding-left: 1rem;
  border-radius: 8px;
}

.review-comment p {
  font-weight: 600;
  color: var(--color-secondary);
  font-size: 14px;
}
</style>
