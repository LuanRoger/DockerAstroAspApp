import { LoaderCircle } from "lucide-react";
import { Button } from "./ui/button";

interface AsyncButtonProps {
  isLoading: boolean;
  text: string
}

export default function AsyncFormButton({
  isLoading,
  text
}: AsyncButtonProps) {
  return isLoading ? (
    <Button disabled>
      <LoaderCircle className="mr-3 animate-spin" />
      Carregando...
    </Button>
  ) : (
    <Button type="submit">{text}</Button>
  );
}
