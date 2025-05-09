import { z } from 'zod'

export const jobFormSchema = z.object({
  id: z.string(),
  position: z.string().min(1, 'Position is required'),
  companyName: z.string().min(1, 'Company name is required'),
  applicationDate: z.string().refine(val => !isNaN(Date.parse(val)), {
    message: 'Invalid date',
  }),
  statusId: z
    .number()
    .min(1, 'Status is required')
    .max(5, 'Status is required'),
})

export type JobFormValues = z.infer<typeof jobFormSchema>
