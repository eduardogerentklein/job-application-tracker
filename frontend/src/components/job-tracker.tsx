'use client'

import { useState, useEffect } from 'react'
import { Loader2, PlusCircle } from 'lucide-react'
import { Button } from '@/components/ui/button'
import { Card, CardContent } from '@/components/ui/card'
import JobList from '@/components/job-list'
import JobForm from '@/components/job-form'
import type { JobApplication, ApplicationStatus } from '@/lib/types'
import {
  getJobApplications,
  createJobApplication,
  updateJobApplication,
  deleteJobApplication,
} from '@/services/jobApplicationService'

import { getApplicationStatuses } from '@/services/jobApplicationStatusService'

export default function JobTracker() {
  const [jobs, setJobs] = useState<JobApplication[]>([])
  const [statuses, setStatuses] = useState<ApplicationStatus[]>([])
  const [isFormOpen, setIsFormOpen] = useState(false)
  const [editingJob, setEditingJob] = useState<JobApplication | null>(null)
  const [loading, setLoading] = useState(false)
  const [refresh, setRefresh] = useState(false)
  const [pageSize] = useState(10)
  const [pageNumber, setPageNumber] = useState(1)
  const [totalCount, setTotalCount] = useState(0)

  useEffect(() => {
    const fetchJobStatus = async () => {
      setLoading(true)
      try {
        const data = await getApplicationStatuses()
        setStatuses(data)
      } catch (err) {
        console.error('Failed to fetch status:', err)
      } finally {
        setLoading(false)
      }
    }
    fetchJobStatus()
  }, [])

  useEffect(() => {
    const fetchJobApplications = async () => {
      setLoading(true)
      try {
        const data = await getJobApplications(pageNumber, pageSize)
        setJobs(data.items)
        setTotalCount(data.totalCount)
      } catch (err) {
        console.error('Failed to fetch job applications:', err)
      } finally {
        setLoading(false)
      }
    }
    fetchJobApplications()
  }, [refresh])

  const addJob = (job: JobApplication) => {
    const addJobApplication = async () => {
      setLoading(true)
      try {
        await createJobApplication(job)
        setRefresh(prev => !prev)
      } catch (err) {
        console.error('Failed to create job application:', err)
      } finally {
        setLoading(false)
        setIsFormOpen(false)
      }
    }
    addJobApplication()
  }

  const updateJob = (updatedJob: JobApplication) => {
    const updateJobApp = async () => {
      setLoading(true)
      try {
        await updateJobApplication(updatedJob.id, updatedJob)
        setRefresh(prev => !prev)
      } catch (err) {
        console.error('Failed to update job application:', err)
      } finally {
        setLoading(false)
        setEditingJob(null)
        setIsFormOpen(false)
      }
    }
    updateJobApp()
  }

  const deleteJob = (id: string) => {
    const updateJobApp = async () => {
      setLoading(true)
      try {
        await deleteJobApplication(id)
        setRefresh(prev => !prev)
      } catch (err) {
        console.error('Failed to delete job application:', err)
      } finally {
        setLoading(false)
      }
    }
    updateJobApp()
  }

  const handleEdit = (job: JobApplication) => {
    setEditingJob(job)
    setIsFormOpen(true)
  }

  return (
    <div className="space-y-6">
      <div className="flex flex-col sm:flex-row justify-between items-start sm:items-center gap-4">
        <div>
          <h1 className="text-3xl font-bold tracking-tight">
            Job Application Tracker
          </h1>
          <p className="text-muted-foreground mt-1">
            Keep track of your job applications, interviews, and offers
          </p>
        </div>
        <Button
          onClick={() => {
            setIsFormOpen(true)
            setEditingJob(null)
          }}
        >
          <PlusCircle className="mr-2 h-4 w-4" />
          Add Application
        </Button>
      </div>
      {loading ? (
        <div className="fixed inset-0 z-50 flex items-center justify-center bg-white/80 backdrop-blur-sm">
          <Loader2 className="h-12 w-12 animate-spin text-gray-700" />
        </div>
      ) : isFormOpen ? (
        <Card>
          <CardContent className="pt-6">
            <JobForm
              statuses={statuses}
              onSubmit={editingJob ? updateJob : addJob}
              onCancel={() => {
                setIsFormOpen(false)
                setEditingJob(null)
              }}
              initialData={editingJob}
            />
          </CardContent>
        </Card>
      ) : jobs.length > 0 ? (
        <JobList
          onPageChange={page => {
            setPageNumber(page)
            setRefresh(prev => !prev)
          }}
          pageNumber={pageNumber}
          pageSize={pageSize}
          totalCount={totalCount}
          statuses={statuses}
          jobs={jobs}
          onEdit={handleEdit}
          onDelete={deleteJob}
        />
      ) : (
        <div className="flex flex-col items-center justify-center py-10 text-center">
          <div className="text-muted-foreground mb-2">
            No job applications found
          </div>
          <p className="text-sm text-muted-foreground">
            Add your first job application to start tracking your progress
          </p>
        </div>
      )}
    </div>
  )
}
