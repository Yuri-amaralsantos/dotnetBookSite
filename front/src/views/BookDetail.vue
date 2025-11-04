<template>
  <div v-if="book" class="book-detail-container">
    <div class="book-header">
      <img :src="book.image" alt="Capa do livro" />
      <div class="book-info">
        <h2>{{ book.title }}</h2>
        <p><strong>Autor:</strong> {{ book.authors?.join(', ') }}</p>
        <p><strong>Publicado:</strong> {{ book.publishedDate }}</p>
      </div>
    </div>

    <form @submit.prevent="submitReview" class="review-form">
      <div class="rating-circles">
        <span v-for="n in 5" :key="n" :class="['circle', { selected: rating === n }]" @click="rating = n">{{ n }}</span>
      </div>
      <textarea v-model="comment" placeholder="Comentário"></textarea>
      <button type="submit">Enviar review</button>
    </form>

    <div class="other-reviews">
      <h3>Outras reviews</h3>
      <ul v-if="reviews.length">
        <li v-for="r in reviews" :key="r.id" class="review-card">
          <p><b>{{ r.userName }}</b>: {{ r.comment }}</p>
          <p>⭐ {{ r.rating }}/5</p>
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
import useAuth from '../components/useAuth'

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


async function submitReview() {
  try {
    const resp = await Api.post('/ReadBooks', {
      GoogleBookId: route.params.id,
      Rating: rating.value || null,
      Comment: comment.value
    }, { headers })

    console.log(resp)

    rating.value = 0
    comment.value = ''
    await fetchBook()
  } catch (error) {
    console.error('Erro ao enviar review', error)
  }
}

onMounted(fetchBook)
</script>
