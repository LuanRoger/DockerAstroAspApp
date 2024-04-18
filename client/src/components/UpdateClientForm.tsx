import {
  clientFormSchema,
  type ClientFormValues,
} from "@/lib/schemas/client_form_schema";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { Form, FormControl, FormField, FormLabel, FormMessage } from "./ui/form";
import type Client from "@/services/models/client";
import { Input } from "./ui/input";
import AsyncFormButton from "./AsyncFormButton";

interface UpdateClientFormProps {
  clientToUpdate: Client;
  onSubmitClick?: (values: ClientFormValues) => void;
  isLoadingButton?: boolean;
}

export default function UpdateClientForm({
  onSubmitClick,
  isLoadingButton = false,
  clientToUpdate,
}: UpdateClientFormProps) {
  const form = useForm<ClientFormValues>({
    resolver: zodResolver(clientFormSchema),
    defaultValues: {
      name: clientToUpdate.name,
      email: clientToUpdate.email,
    },
  });

  function onSubmit(value: ClientFormValues) {
    onSubmitClick?.(value);
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
          <AsyncFormButton isLoading={isLoadingButton} text="Editar"/>
        </div>
      </form>
    </Form>
  );
}
