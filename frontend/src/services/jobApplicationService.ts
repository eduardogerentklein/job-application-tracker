import axiosClient from '@/api/axiosClient'
import { endpoints } from '@/api/endpoints'
import {
  JobApplication,
  UpdateJobApplicationRequest,
  CreateJobApplicationRequest,
  PaginatedJobApplications,
} from '@/lib/types'

export const createJobApplication = async (
  data: CreateJobApplicationRequest
): Promise<string> => {
  const response = await axiosClient.post<string>(endpoints.applications, data)
  console.log('response', response)
  return response.data
}

// Get job applications with pagination
export const getJobApplications = async (
  pageNumber: number,
  pageSize: number
): Promise<PaginatedJobApplications> => {
  const response = await axiosClient.get<PaginatedJobApplications>(
    endpoints.applications,
    {
      params: {
        pageNumber,
        pageSize,
      },
    }
  )
  return response.data
}

// Get a specific job application by ID
export const getJobApplicationById = async (
  id: string
): Promise<JobApplication> => {
  const response = await axiosClient.get<JobApplication>(
    `${endpoints.applications}/${id}`
  )
  return response.data
}

// Update a job application by ID
export const updateJobApplication = async (
  id: string,
  data: UpdateJobApplicationRequest
): Promise<JobApplication> => {
  const response = await axiosClient.put<JobApplication>(
    `${endpoints.applications}/${id}`,
    data
  )
  return response.data
}

// Delete a job application by ID
export const deleteJobApplication = async (id: string): Promise<void> => {
  await axiosClient.delete(`${endpoints.applications}/${id}`)
}
