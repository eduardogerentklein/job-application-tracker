'use client'

import { useState } from 'react'
import { Calendar, Edit, MoreHorizontal, Trash2 } from 'lucide-react'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from '@/components/ui/table'
import { Button } from '@/components/ui/button'
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu'
import {
  Pagination,
  PaginationContent,
  PaginationItem,
  PaginationLink,
  PaginationNext,
  PaginationPrevious,
} from '@/components/ui/pagination'

import type { JobApplication, ApplicationStatus } from '@/lib/types'
import { formatDate, formatStatus } from '@/lib/utils'
import { Badge } from './ui/badge'

interface JobListProps {
  jobs: JobApplication[]
  onEdit: (job: JobApplication) => void
  onDelete: (id: string) => void
  onPageChange: (pageNumber: number) => void
  statuses?: ApplicationStatus[] | null
  pageSize: number
  pageNumber: number
  totalCount: number
}

export default function JobList({
  jobs,
  onEdit,
  onDelete,
  statuses,
  pageSize,
  pageNumber,
  totalCount,
  onPageChange,
}: JobListProps) {
  const [sortField, setSortField] = useState<'applicationDate' | 'company'>(
    'applicationDate'
  )
  const [sortDirection, setSortDirection] = useState<'asc' | 'desc'>('desc')
  const [totalPages] = useState(Math.ceil(totalCount / pageSize))

  const handleSort = (field: 'applicationDate' | 'company') => {
    if (sortField === field) {
      setSortDirection(sortDirection === 'asc' ? 'desc' : 'asc')
    } else {
      setSortField(field)
      setSortDirection('asc')
    }
  }

  const handlePageChange = (page: number) => {
    onPageChange(page)
  }

  const sortedJobs = [...jobs].sort((a, b) => {
    if (sortField === 'applicationDate') {
      const dateA = new Date(a.applicationDate).getTime()
      const dateB = new Date(b.applicationDate).getTime()
      return sortDirection === 'asc' ? dateA - dateB : dateB - dateA
    } else {
      return sortDirection === 'asc'
        ? a.companyName.localeCompare(b.companyName)
        : b.companyName.localeCompare(a.companyName)
    }
  })

  if (jobs.length === 0) {
    return (
      <div className="flex flex-col items-center justify-center py-10 text-center">
        <div className="text-muted-foreground mb-2">
          No job applications found
        </div>
        <p className="text-sm text-muted-foreground">
          Add your first job application to start tracking your progress
        </p>
      </div>
    )
  }

  return (
    <div className="rounded-md border">
      <Table>
        <TableHeader>
          <TableRow>
            <TableHead>Position</TableHead>
            <TableHead
              className="cursor-pointer hover:text-primary"
              onClick={() => handleSort('company')}
            >
              Company{' '}
              {sortField === 'company' && (sortDirection === 'asc' ? '↑' : '↓')}
            </TableHead>
            <TableHead
              className="cursor-pointer hover:text-primary"
              onClick={() => handleSort('applicationDate')}
            >
              Date Applied{' '}
              {sortField === 'applicationDate' &&
                (sortDirection === 'asc' ? '↑' : '↓')}
            </TableHead>
            <TableHead>Status</TableHead>
            <TableHead className="text-right">Actions</TableHead>
          </TableRow>
        </TableHeader>
        <TableBody>
          {sortedJobs.map(job => (
            <TableRow key={job.id}>
              <TableCell className="font-medium">{job.position}</TableCell>
              <TableCell>{job.companyName}</TableCell>
              <TableCell>
                <div className="flex items-center">
                  <Calendar className="mr-2 h-4 w-4 text-muted-foreground" />
                  {formatDate(job.applicationDate)}
                </div>
              </TableCell>
              <TableCell>
                <Badge variant="outline">
                  {formatStatus(job.statusId, statuses)}
                </Badge>
              </TableCell>
              <TableCell className="text-right">
                <DropdownMenu>
                  <DropdownMenuTrigger asChild>
                    <Button variant="ghost" size="icon">
                      <MoreHorizontal className="h-4 w-4" />
                      <span className="sr-only">Open menu</span>
                    </Button>
                  </DropdownMenuTrigger>
                  <DropdownMenuContent align="end">
                    <DropdownMenuItem onClick={() => onEdit(job)}>
                      <Edit className="mr-2 h-4 w-4" />
                      Edit
                    </DropdownMenuItem>
                    <DropdownMenuItem
                      className="text-destructive focus:text-destructive"
                      onClick={() => onDelete(job.id)}
                    >
                      <Trash2 className="mr-2 h-4 w-4" />
                      Delete
                    </DropdownMenuItem>
                  </DropdownMenuContent>
                </DropdownMenu>
              </TableCell>
            </TableRow>
          ))}
        </TableBody>
      </Table>
      <Pagination>
        <PaginationContent>
          <PaginationItem>
            <PaginationPrevious
              href="#"
              onClick={() => handlePageChange(Math.max(1, pageNumber - 1))}
              className={
                pageNumber === 1 ? 'pointer-events-none opacity-50' : ''
              }
            />
          </PaginationItem>
          {Array.from({ length: totalPages }, (_, i) => {
            const page = i + 1
            return (
              <PaginationItem key={page}>
                <PaginationLink
                  href={`#${page}`}
                  isActive={page === pageNumber}
                  onClick={() => handlePageChange(page)}
                >
                  {page}
                </PaginationLink>
              </PaginationItem>
            )
          })}
          <PaginationItem>
            <PaginationNext
              href="#"
              onClick={() =>
                handlePageChange(Math.min(totalPages, pageNumber + 1))
              }
              className={
                pageNumber === totalPages
                  ? 'pointer-events-none opacity-50'
                  : ''
              }
            />
          </PaginationItem>
        </PaginationContent>
      </Pagination>
    </div>
  )
}
