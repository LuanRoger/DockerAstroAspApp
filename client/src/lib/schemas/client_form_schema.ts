import { z } from "zod";

export const clientFormSchema = z.object({
  name: z.string().min(1),
  email: z.string().email(),
});

export type ClientFormValues = z.infer<
  typeof clientFormSchema
>;
