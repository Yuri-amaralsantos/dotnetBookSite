import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  plugins: [vue()],
  server: {
    proxy: {
      '/Api': 'http://localhost:5000' // substitua pela URL/porta do seu backend .NET
    }
  }
})
