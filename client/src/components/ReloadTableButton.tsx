import page_store from "../stores/page_store";

export default function ReloadTableButton() {
  function resetPage() {
    const page = page_store.get();
    page_store.set(page);
  }

  return (
    <button
      className="transition-colors hover:bg-slate-500 p-3 rounded-md"
      onClick={resetPage}
    >
      Recarregar tabela
    </button>
  );
}
