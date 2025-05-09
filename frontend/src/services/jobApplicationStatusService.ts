import axiosClient from '@/api/axiosClient'
import { endpoints } from '@/api/endpoints'
import { ApplicationStatus } from '@/lib/types'

// Get job applications with pagination
export const getApplicationStatuses = async (): Promise<
  ApplicationStatus[]
> => {
  const response = await axiosClient.get<ApplicationStatus[]>(
    endpoints.applicationStatuses
  )
  return response.data
}
