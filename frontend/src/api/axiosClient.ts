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
    const status = error.response?.status || 500
    const message = error.response?.data?.detail || 'Unexpected error occurred'
    const checkErrorStatusFamily = (
      currentStatus: number,
      statusGroup: number
    ) => Math.floor(currentStatus / 100) === statusGroup
    if (checkErrorStatusFamily(status, 4)) toast(message)
    else if (checkErrorStatusFamily(status, 5)) toast(message)

    return Promise.reject(error)
  }
)

export default axiosClient
