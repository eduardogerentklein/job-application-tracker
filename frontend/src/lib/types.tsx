export interface JobApplication {
  id: string
  position: string
  companyName: string
  applicationDate: string
  statusId: number
}

export type ApplicationStatus = {
  id: number
  name: string
}

export interface CreateJobApplicationRequest {
  position: string
  companyName: string
  applicationDate: string
  statusId: number
}

export interface UpdateJobApplicationRequest {
  position?: string
  companyName?: string
  applicationDate?: string
  statusId?: number
}

export interface PaginatedJobApplications {
  items: JobApplication[]
  totalCount: number
  pageSize: number
  pageNumber: number
}
