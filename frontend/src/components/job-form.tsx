'use client'

import type React from 'react'

import { useState } from 'react'
import { CalendarIcon } from 'lucide-react'
import { format } from 'date-fns'
import { Button } from '@/components/ui/button'
import { Input } from '@/components/ui/input'
import { Label } from '@/components/ui/label'
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from '@/components/ui/select'
import {
  Popover,
  PopoverContent,
  PopoverTrigger,
} from '@/components/ui/popover'
import { Calendar } from '@/components/ui/calendar'
import type { ApplicationStatus, JobApplication } from '@/lib/types'
import { jobFormSchema, JobFormValues } from '@/lib/schemas/jobForm'
import { ZodError } from 'zod'

interface JobFormProps {
  onSubmit: (job: JobApplication) => void
  onCancel: () => void
  initialData?: JobFormValues | null
  statuses?: ApplicationStatus[] | null
}

export default function JobForm({
  onSubmit,
  onCancel,
  initialData,
  statuses,
}: JobFormProps) {
  const [formData, setFormData] = useState<Omit<JobFormValues, 'id'>>({
    position: initialData?.position || '',
    companyName: initialData?.companyName || '',
    applicationDate:
      initialData?.applicationDate || format(new Date(), 'yyyy-MM-dd'),
    statusId: initialData?.statusId || 1,
  })

  const [errors, setErrors] = useState<
    Partial<Record<keyof JobFormValues, string>>
  >({})

  const [date, setDate] = useState<Date | undefined>(
    initialData?.applicationDate
      ? new Date(initialData.applicationDate)
      : new Date()
  )

  const handleChange = (
    e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>
  ) => {
    const { name, value } = e.target
    setFormData({ ...formData, [name]: value })
  }

  const handleStatusChange = (value: string) => {
    setFormData({ ...formData, statusId: Number(value) })
  }

  const handleDateChange = (date: Date | undefined) => {
    if (date) {
      setDate(date)
      setFormData({
        ...formData,
        applicationDate: format(date, 'yyyy-MM-dd'),
      })
    }
  }

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault()
    try {
      const parsedData = jobFormSchema.parse(
        initialData
          ? { ...formData, id: initialData.id }
          : { ...formData, id: '' }
      )
      onSubmit(parsedData)
    } catch (error) {
      if (error instanceof ZodError) {
        const fieldErrors: Partial<Record<keyof JobFormValues, string>> = {}
        error.errors.forEach(e => {
          if (e.path[0])
            fieldErrors[e.path[0] as keyof JobFormValues] = e.message
        })
        setErrors(fieldErrors)
      }
    }
  }

  return (
    <form onSubmit={handleSubmit} className="space-y-6">
      <h2 className="text-xl font-semibold">
        {initialData ? 'Edit Job Application' : 'Add New Job Application'}
      </h2>

      <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
        <div className="space-y-2">
          <Label htmlFor="position">Position</Label>
          <Input
            id="position"
            name="position"
            value={formData.position}
            onChange={handleChange}
            placeholder="Software Engineer"
          />
          {errors.position && <p className="text-red-500">{errors.position}</p>}
        </div>

        <div className="space-y-2">
          <Label htmlFor="companyName">Company</Label>
          <Input
            id="companyName"
            name="companyName"
            value={formData.companyName}
            onChange={handleChange}
            placeholder="Datacom"
          />
          {errors.companyName && (
            <p className="text-red-500">{errors.companyName}</p>
          )}
        </div>

        <div className="space-y-2">
          <Label htmlFor="applicationDate">Date Applied</Label>
          <Popover>
            <PopoverTrigger asChild>
              <Button
                variant="outline"
                className="w-full justify-start text-left font-normal"
              >
                <CalendarIcon className="mr-2 h-4 w-4" />
                {date ? format(date, 'PPP') : 'Select date'}
              </Button>
            </PopoverTrigger>
            <PopoverContent className="w-auto p-0">
              <Calendar
                mode="single"
                selected={date}
                onSelect={handleDateChange}
                initialFocus
              />
            </PopoverContent>
          </Popover>
          {errors.applicationDate && (
            <p className="text-red-500">{errors.applicationDate}</p>
          )}
        </div>

        <div className="space-y-2">
          <Label htmlFor="status">Status</Label>
          <Select
            value={`${formData.statusId}`}
            onValueChange={value => handleStatusChange(value)}
          >
            <SelectTrigger>
              <SelectValue placeholder="Select status" />
            </SelectTrigger>
            <SelectContent>
              {statuses?.map(status => (
                <SelectItem key={status.id} value={`${status.id}`}>
                  <div className="flex items-center">{status.name}</div>
                </SelectItem>
              ))}
            </SelectContent>
          </Select>
          {errors.statusId && <p className="text-red-500">{errors.statusId}</p>}
        </div>
      </div>

      <div className="flex justify-end gap-2">
        <Button type="button" variant="outline" onClick={onCancel}>
          Cancel
        </Button>
        <Button type="submit">
          {initialData ? 'Update' : 'Add'} Application
        </Button>
      </div>
    </form>
  )
}
