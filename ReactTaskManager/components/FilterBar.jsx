export default function FilterBar({ filter, setFilter, unfinishedTaskCount, deleteCompleted }) {
    return (
        <fieldset>
            <legend>Wybierz typ filtrowania:</legend>
            <div>
                <label>
                    <input type="radio" name="filter" value="Wszystkie" checked={filter === "All"} onChange={() => setFilter("All")} />
                    Wszystkie
                </label>
            </div>
            <div>
                <label>
                    <input type="radio" name="filter" value="Zakończone" checked={filter === "Completed"} onChange={() => setFilter("Completed")} />
                    Zakończone
                </label>
            </div>
            <div>
                <label>
                    <input type="radio" name="filter" value="Niezakończone" checked={filter === "Uncompleted"} onChange={() => setFilter("Uncompleted")} />
                    Niezakończone
                </label>
            </div>
            <div>
                <p>{"Liczba bieżących zadań: " + unfinishedTaskCount}</p>
            </div>
            <div>
                <button onClick={deleteCompleted}>Usuń zakończone</button>
            </div>
        </fieldset>
    );
}