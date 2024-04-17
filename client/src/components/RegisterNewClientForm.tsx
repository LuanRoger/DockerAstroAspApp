import { z } from "zod";
import {
  Form,
  FormControl,
  FormField,
  FormLabel,
  FormMessage,
} from "./ui/form";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Input } from "./ui/input";
import { Button } from "./ui/button";
import type { RegisterClient } from "@/lib/interfaces/register_client";
import { LoaderCircle } from "lucide-react";

const registerNewClientFormSchema = z.object({
  name: z.string().min(1),
  email: z.string().email(),
});

type RegisterNewClientFormValues = z.infer<typeof registerNewClientFormSchema>;

interface RegisterNewClientFormProps {
  onSubmitClick: (values: RegisterClient) => void;
  isLoadingButton?: boolean;
}

export default function RegisterNewClientForm({
  onSubmitClick,
  isLoadingButton = false,
}: RegisterNewClientFormProps) {
  const form = useForm<RegisterNewClientFormValues>({
    resolver: zodResolver(registerNewClientFormSchema),
    defaultValues: {
      name: "",
      email: "",
    },
  });

  function onSubmit(value: RegisterNewClientFormValues) {
    onSubmitClick(value);
  }

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-8">
        <FormField
          control={form.control}
          name="name"
          render={({ field }) => (
            <FormLabel>
              <FormLabel>Nome</FormLabel>
              <FormControl>
                <Input placeholder="Nome" {...field}></Input>
              </FormControl>
              <FormMessage />
            </FormLabel>
          )}
        ></FormField>
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormLabel>
              <FormLabel>Email</FormLabel>
              <FormControl>
                <Input placeholder="Email" {...field}></Input>
              </FormControl>
              <FormMessage />
            </FormLabel>
          )}
        ></FormField>
        <div className="flex justify-end">
          {isLoadingButton ? (
            <Button disabled>
              <LoaderCircle className="mr-3 animate-spin" />
              Carregando...
            </Button>
          ) : (
            <Button type="submit">Cadastrar</Button>
          )}
        </div>
      </form>
    </Form>
  );
}
