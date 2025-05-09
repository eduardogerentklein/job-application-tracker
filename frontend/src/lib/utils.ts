import { clsx, type ClassValue } from 'clsx'
import { twMerge } from 'tailwind-merge'
import { ApplicationStatus } from '@/lib/types'

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function formatDate(dateString: string): string {
  const date = new Date(dateString)
  return new Intl.DateTimeFormat('en-US', {
    month: 'short',
    day: 'numeric',
    year: 'numeric',
  }).format(date)
}

export function formatStatus(
  statusId: number,
  statuses?: ApplicationStatus[] | null
): string {
  const status = statuses?.find(s => s.id === statusId)
  return status ? status.name : 'Unknown'
}
