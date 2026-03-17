export default function FilterBar({ filter, setFilter, unfinishedTaskCount, deleteCompleted }) {
    return (
        <fieldset>
            <legend>Filter</legend>
            <div>
                <label className="radio-label">
                    <input className="radio" type="radio" name="filter" value="Wszystkie" checked={filter === "All"} onChange={() => setFilter("All")} />
                    Wszystkie
                </label>
            </div>
            <div>
                <label className="radio-label">
                    <input className="radio" type="radio" name="filter" value="Zakończone" checked={filter === "Completed"} onChange={() => setFilter("Completed")} />
                    Zakończone
                </label>
            </div>
            <div>
                <label className="radio-label">
                    <input className="radio" type="radio" name="filter" value="Niezakończone" checked={filter === "Uncompleted"} onChange={() => setFilter("Uncompleted")} />
                    Niezakończone
                </label>
            </div>
            <div>
                <p>{"Liczba bieżących zadań: " + unfinishedTaskCount}</p>
            </div>
            <div>
                <button className="button" onClick={deleteCompleted}>Usuń zakończone</button>
            </div>
        </fieldset>
    );
}