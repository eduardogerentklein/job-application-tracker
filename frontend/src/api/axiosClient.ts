import axios from 'axios'
import { toast } from 'sonner'

const axiosClient = axios.create({
  baseURL: process.env.NEXT_PUBLIC_API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
})

axiosClient.interceptors.response.use(
  response => response,
  error => {
    toast(error.response?.data.detail || 'Unexpected error occurred')
    return Promise.reject(error)
  }
)

export default axiosClient
