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
import { clientFormSchema, type ClientFormValues } from "@/lib/schemas/client_form_schema";
import AsyncFormButton from "./AsyncFormButton";

interface RegisterNewClientFormProps {
  onSubmitClick: (values: RegisterClient) => void;
  isLoadingButton?: boolean;
}

export default function RegisterNewClientForm({
  onSubmitClick,
  isLoadingButton = false,
}: RegisterNewClientFormProps) {
  const form = useForm<ClientFormValues>({
    resolver: zodResolver(clientFormSchema),
    defaultValues: {
      name: "",
      email: "",
    },
  });

  function onSubmit(value: ClientFormValues) {
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
          <AsyncFormButton isLoading={isLoadingButton} text="Cadastrar"/>
        </div>
      </form>
    </Form>
  );
}
